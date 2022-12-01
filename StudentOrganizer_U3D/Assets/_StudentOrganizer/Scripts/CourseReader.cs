using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleFileBrowser;
using UnityEngine;

public class CourseReader : MonoBehaviour {
    private void Start() {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Course Files", ".json", ".csv"));
        FileBrowser.AddQuickLink( "C", "C:", null );
        StartCoroutine( ShowLoadDialogCoroutine() );
    }

    IEnumerator ShowLoadDialogCoroutine() {
        yield return FileBrowser.WaitForLoadDialog( FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load" );
        if( FileBrowser.Success )
        {
            for( int i = 0; i < FileBrowser.Result.Length; i++ )
                Debug.Log( FileBrowser.Result[i] );

            byte[] bytes = FileBrowserHelpers.ReadBytesFromFile( FileBrowser.Result[0] );

            string destinationPath = Path.Combine( Application.persistentDataPath, FileBrowserHelpers.GetFilename( FileBrowser.Result[0] ) );
            FileBrowserHelpers.CopyFile( FileBrowser.Result[0], destinationPath );
        }
    }
}