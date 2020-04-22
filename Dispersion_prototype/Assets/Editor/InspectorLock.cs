using UnityEditor;

static class EditorMenus
{
    [MenuItem("Tools/Toggle Inspector Lock &q")] // Alt + Q
    static void ToggleInspectorLock()
    {
        ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
        ActiveEditorTracker.sharedTracker.ForceRebuild();
    }
}