# IDEA Mixed Reality Room Planner

Using Unity 6 and Meta's XR SDK, I developed a mixed reality app for the Quest 3 that allows users to visualize and customize their living spaces with life-sized 3D furniture models, in real time. 

The app features advanced hand tracking, giving users the freedom to grab, move, and arrange furniture as they would naturally, without the need for controllers. Users can grab the virtual catalogue panel to view a selection of over 100+ unique pieces, including armchairs, desks, drawers, lamps, bookcases, and sofas. 

This mixed reality experience offers a new way to explore interior design, making it more intuitive in a natural, hands-on way.

---

### Disclaimer

Due to the large file size of .glb 3D models, a good amount of assets were excluded from this repository.

Also, in the project files you can tell the project was originally called ```PROJECT-MR```, this was before I realized IDEA MR Room Planner was a lot cooler (the logo is from Escape from Tarkov).

## Features

- **Life-Sized 3D Models:** Visualize furniture at true scale within your actual space.
- **Advanced Hand Tracking:** Utilize natural hand gestures to grab, move, and rotate objects without controllers.
- **Extensive Catalogue:** Browse over 100 unique items, including:
  - Armchairs
  - Desks
  - Drawers
  - Lamps
  - Bookcases
  - Sofas
- **Intuitive UI:** Access the virtual catalogue panel to select and preview items.
- **Real-Time Interaction:** Experience immediate feedback when interacting with virtual objects.

---

## Technologies

- **Unity 6:** Core development engine.
- **Meta's XR SDK:** Provides hand tracking and XR integration.
- **Quest 3:** Target mixed reality hardware.

---

## Installation and Setup

### Prerequisites

- **Unity 6:** Install from the [Unity Download Archive](https://unity3d.com/get-unity/download/archive).
- **Meta's XR SDK:** Install via the Unity Package Manager or download from [Meta’s Developer Portal](https://developer.oculus.com/).
- **Quest 3 Headset:** For deployment and testing.
- **Android Build Support:** Ensure Android SDK, NDK, and OpenJDK are installed (available with Unity installation).

### Cloning the Repository

1. Open your terminal or command prompt.
2. Clone the repository:
   ```bash
   git clone https://github.com/alejandrocqvd/IDEA-MR-Room-Planner.git
   ```
3. Change into the project directory:
   ```bash
   cd IDEA-MR-Room-Planner
   ```

### Unity Project Setup

1. **Open the Project:**
   - Launch Unity Hub.
   - Click **"Add"** and navigate to the cloned project folder.
   - Open the project in Unity 6.

2. **Install Meta's XR SDK:**
   - Open **Window > Package Manager**.
   - Find and install the Meta (Oculus) XR Plugin.
   - Configure XR Plugin Management via **Edit > Project Settings > XR Plugin Management** and enable the Oculus plugin under the Android tab.

3. **Configure Build Settings:**
   - Go to **File > Build Settings**.
   - Select **Android** as the target platform and click **"Switch Platform"**.
   - Verify that your Android SDK, NDK, and OpenJDK are properly set up.
   - Set the Minimum API Level to 23 (or higher as required by Quest 3).

4. **Scene Setup:**
   - Open the main scene.
   - Ensure the XR rig, hand tracking components, and UI elements (catalogue panel, 3D models) are correctly configured.

---

## Running the Application

**Unity Editor Play Mode:**
   - Click the **"Play"** button to run the scene.
   - Ensure your Quest 3 is connected in Unity.
   - Verify that UI elements, object interactions, and catalogue browsing work as expected.

---

## Acknowledgments

- **Unity:** The engine powering this project.
- **Meta's XR SDK:** Enabling advanced hand tracking and XR integration.
- **Quest 3:** Providing the hardware platform for an immersive experience.
