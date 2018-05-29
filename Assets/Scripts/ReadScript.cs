using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class ReadScript{

	public static T Read<T>(string fileName){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file;
		if (File.Exists (Application.persistentDataPath + "/"+fileName+".dat")) {
			file = File.Open (Application.persistentDataPath + "/"+fileName+".dat", FileMode.Open);
			T content = (T)bf.Deserialize (file);
			file.Close ();
			return content;
		}
		return default(T);
	}
}
