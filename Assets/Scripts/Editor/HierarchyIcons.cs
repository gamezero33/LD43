using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;

[InitializeOnLoad]
class HierarchyIcons
{
	static Texture2D[] icons;
	static List<int> markedObjects;

	static HierarchyIcons ()
	{
		EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemCB;
		//icons = GetAtPath<Texture2D>( "Editor/Hierarchy Icons" );
	}

	static void HierarchyItemCB ( int instanceID, Rect selectionRect )
	{
		Rect r = new Rect( selectionRect );
		r.x -= 30;
		r.width = 20;

		GameObject go = EditorUtility.InstanceIDToObject( instanceID ) as GameObject;
		GUIContent gc = EditorGUIUtility.ObjectContent( go, typeof( GameObject ) );

		if ( gc.image.name != "GameObject Icon" )
		{
			GUI.Label( r, gc.image );
		}
		else if ( go && icons != null && icons.Length > 0 )
		{
			for ( int i = 0; i < icons.Length; i++ )
			{
				if ( go.name.Contains( icons[i].name ) )
				{
					GUI.Label( r, icons[i] );
				}
			}
		}

	}

	#region - Get At Path -
	public static T[] GetAtPath<T> ( string path )
	{

		ArrayList al = new ArrayList();
		string[] fileEntries = Directory.GetFiles( Application.dataPath + "/" + path );
		foreach ( string fileName in fileEntries )
		{
			int assetPathIndex = fileName.IndexOf( "Assets" );
			string localPath = fileName.Substring( assetPathIndex );

			Object t = AssetDatabase.LoadAssetAtPath( localPath, typeof( T ) );

			if ( t != null )
				al.Add( t );
		}
		T[] result = new T[al.Count];
		for ( int i = 0; i < al.Count; i++ )
			result[i] = (T)al[i];

		return result;
	}
	#endregion
}