using UnityEditor;
using UnityEngine.SceneManagement;

/// <summary>
/// Class <c>Helpers</c> contains common reusable functions that are useful across all scenes and classes.
/// </summary>
public static class Helpers
{
    /// <summary>
    /// Move to the scene listed after the current scene listed in Build Profiles > Scene List
    /// </summary>
    public static void NextScreen()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    /// <summary>
    /// Move to the scene listed before the current scene listed in Build Profiles > Scene List
    /// </summary>
    public static void PrevScreen()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    /// <summary>
    /// Move to the scene with the specified index listed in Build Profiles > Scene List
    /// </summary>
    /// <param name="i">A integer representing the index.</param>
    public static void GoToScreen(int i)
    {
        SceneManager.LoadScene(i);
    }
    /// <summary>
    /// Move to the scene with the specified name.
    /// </summary>
    /// <param name="name">A string representing the scene name.</param>
    public static void GoToScreen(string name)
    {
        SceneManager.LoadScene(name);
    }
    /// <summary>
    /// Move to the scene of the SceneAsset.
    /// </summary>
    /// <param name="scene">A SceneAsset object representing the scene to move to.</param>
    public static void GoToScreen(SceneAsset scene)
    {
        SceneManager.LoadScene(scene.name);
    }
}
