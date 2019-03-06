using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.IO;

public class Serializer
{
	public static bool Serialize<T>(T value, ref string serializeXml)
	{
		if (value == null)
		{
			return false;
		}
		try
		{
			XmlSerializer xmlserializer = new XmlSerializer(typeof(T));
			StringWriter stringWriter = new StringWriter();
			XmlWriter writer = XmlWriter.Create(stringWriter);
			
			xmlserializer.Serialize(writer, value);
			
			serializeXml = stringWriter.ToString();
			
			writer.Close();
			return true;
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Exception raised: " + ex);
			return false;
		}
	}


	public static bool DeSerialize<T>(string value, ref T deserializedXml)
	{
		if (value == null)
		{
			return false;
		}

		try 
		{
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			StringReader rdr = new StringReader(value);
			deserializedXml = (T) serializer.Deserialize(rdr);

			rdr.Close();
			return true;
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Exception raised: " + ex);
			return false;
		}

	}
}
