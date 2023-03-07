using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace HotelWeb.Validation
{
    public class ProductImageValidation : ValidationAttribute
    {
        public ProductImageValidation()
        {
            
        }

        public override bool IsValid(object value)
        {
            List<string> listExtensions = new List<string>
            {
                ".jpg",
                ".png",
                ".webp"
            };
            bool result = true;
            try
            {
                //decimal price = decimal.Parse(value.ToString());
                IFormFile file = (IFormFile)value;
                if(file != null)
                {
                    string extensions = Path.GetExtension(file.FileName);
                    if(listExtensions.Exists(p => p.Equals(extensions))) {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}
