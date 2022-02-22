﻿using System.Security.Cryptography;
using System.Text;

namespace WebApplication.Code
{
    public class Security
    {
        public static string ComputeSha256Hash(string rawData)  
        {  
            // Create a SHA256   
            using (var sha256Hash = SHA256.Create())  
            {  
                // ComputeHash - returns byte array  
                var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));  
  
                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();  
                foreach (var t in bytes)
                {
                    builder.Append(t.ToString("x2"));
                }  
                return builder.ToString();  
            }  
        } 
    }
}