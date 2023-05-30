using System;
using System.Collections.Generic;

public class TextImporter
{
    public static string DictionaryToString<T1, T2>(Dictionary<T1, T2> _dict)
        {
            string result = "{";
    
            try
            {
                foreach (var keyValuePair in _dict)
                {
                    result += ($" \"{keyValuePair.Key}\" : \"{keyValuePair.Value}\", ");
                }
                result.TrimEnd();
            }
            catch (Exception e)
            {
                result += e.Message;
                throw;
            }
            finally
            {
                result += "}";
            }
            
            return result;
        }
    public static string DictionaryToString(Dictionary<int, int> _dict)
    {
        string result = "{";

        try
        {
            foreach (var keyValuePair in _dict)
            {
                result += ($"{keyValuePair.Key} : {keyValuePair.Value},");
            }

            result.TrimEnd();
        }
        catch (Exception e)
        {
            result += e.Message;
            throw;
        }
        finally
        {
            result += "}";
        }
        
        return result;
    }
}