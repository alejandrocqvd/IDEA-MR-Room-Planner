using UnityEngine;
using UnityEditor;
using System.IO;
using Oculus.Interaction;
using System.Collections.Generic;

public class GLBToPrefab : EditorWindow
{
    private GameObject importedModel;
    private string prefabName = "NewPrefab";

    [MenuItem("Tools/GLB to Prefab")]
    public static void ShowWindow()
    {
        GetWindow<GLBToPrefab>("GLB to Prefab");
    }

    void OnGUI()
    {
        GUILayout.Label("GLB to Prefab Generator", EditorStyles.boldLabel);

        importedModel = (GameObject)EditorGUILayout.ObjectField("Imported Model", importedModel, typeof(GameObject), true);
        prefabName = EditorGUILayout.TextField("Prefab Name", prefabName);

        if (GUILayout.Button("Generate Prefab"))
        {
            if (importedModel != null)
            {
                CreatePrefab();
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "Please assign an imported GLB model!", "OK");
            }
        }
    }

    private void CreatePrefab()
    {
        if (importedModel == null)
        {
            EditorUtility.DisplayDialog("Error", "No model selected!", "OK");
            return;
        }

        // Set Prefab Name
        string prefabNameFull = "PF " + importedModel.name;

        // Create a new GameObject from the imported model
        GameObject newPrefab = Instantiate(importedModel);
        newPrefab.name = prefabNameFull;

        // Ensure there's a Rigidbody on the root and remove any in children
        Rigidbody[] existingRbs = newPrefab.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody existingRb in existingRbs)
        {
            if (existingRb.gameObject != newPrefab)
            {
                DestroyImmediate(existingRb);
            }
        }

        // Add or get Rigidbody on the root
        Rigidbody rb = newPrefab.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = ObjectFactory.AddComponent<Rigidbody>(newPrefab);
        }
        rb.useGravity = false;
        rb.isKinematic = true;

        // Ensure there's a MeshCollider on the root and remove any in children
        MeshCollider[] existingMeshColliders = newPrefab.GetComponentsInChildren<MeshCollider>();
        foreach (MeshCollider existingMeshCollider in existingMeshColliders)
        {
            if (existingMeshCollider.gameObject != newPrefab)
            {
                DestroyImmediate(existingMeshCollider);
            }
        }

        // Add or get MeshCollider on the root
        MeshCollider meshCollider = newPrefab.GetComponent<MeshCollider>();
        if (meshCollider == null)
        {
            meshCollider = ObjectFactory.AddComponent<MeshCollider>(newPrefab);
        }
        meshCollider.convex = true;

        // Ensure there's a BoxCollider on the root and remove any in children
        BoxCollider[] existingBoxColliders = newPrefab.GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider existingBoxCollider in existingBoxColliders)
        {
            if (existingBoxCollider.gameObject != newPrefab)
            {
                DestroyImmediate(existingBoxCollider);
            }
        }

        // Add or get BoxCollider on the root
        BoxCollider boxCollider = newPrefab.GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            boxCollider = ObjectFactory.AddComponent<BoxCollider>(newPrefab);
        }


        // ==================================================================================================================

        // Add GrabFreeTransformer component 
        GrabFreeTransformer grabFreeTransformer = newPrefab.GetComponent<GrabFreeTransformer>();
        if (grabFreeTransformer == null)
        {
            grabFreeTransformer = newPrefab.AddComponent<GrabFreeTransformer>();
        }

        // Create new rotation constraints
        TransformerUtils.RotationConstraints rotationConstraints = new TransformerUtils.RotationConstraints()
        {
            XAxis = new TransformerUtils.ConstrainedAxis()
            {
                ConstrainAxis = true,
                AxisRange = new TransformerUtils.FloatRange() { Min = 0f, Max = 0f }
            },
            YAxis = new TransformerUtils.ConstrainedAxis()
            {
                ConstrainAxis = false
            },
            ZAxis = new TransformerUtils.ConstrainedAxis()
            {
                ConstrainAxis = true,
                AxisRange = new TransformerUtils.FloatRange() { Min = 270f, Max = 90f }
            }
        };

        // Apply the new rotation constraints using the injection method
        grabFreeTransformer.InjectOptionalRotationConstraints(rotationConstraints);

        // ==================================================================================================================

        // Add Grabbable component
        Grabbable grabbable = newPrefab.GetComponent<Grabbable>();
        if (grabbable == null)
        {
            grabbable = newPrefab.AddComponent<Grabbable>();
        }

        // Configure Grabbable
        grabbable.InjectOptionalRigidbody(rb);
        if (grabFreeTransformer != null)
        {
            grabbable.InjectOptionalOneGrabTransformer(grabFreeTransformer);
        }

        // ==================================================================================================================

        // Add TouchHandGrabInteractable component
        TouchHandGrabInteractable touchGrab = newPrefab.GetComponent<TouchHandGrabInteractable>();
        if (touchGrab == null)
        {
            touchGrab = newPrefab.AddComponent<TouchHandGrabInteractable>();
        }

        // Use only the meshCollider as element 0.
        List<Collider> colliders = new List<Collider>();
        colliders.Add(meshCollider);

        // Configure TouchHandGrabInteractable
        touchGrab.InjectAllTouchHandGrabInteractable(boxCollider, colliders);

        // ==================================================================================================================

        // Save as Prefab
        string savePath = "Assets/Furniture/Armchairs";
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        string fullPath = Path.Combine(savePath, prefabNameFull + ".prefab");
        PrefabUtility.SaveAsPrefabAsset(newPrefab, fullPath);

        // Cleanup
        DestroyImmediate(newPrefab);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog("Success", $"Prefab '{prefabNameFull}' created successfully at {fullPath}", "OK");
    }
}
