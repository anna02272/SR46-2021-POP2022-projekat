﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Models

{
    [Serializable]

    public class Language: ICloneable, IDataErrorInfo

    {
        public int Id { get; set; } 
        public string NameOfLanguage { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsValid { get; set; }

        public Language()
        {
            IsDeleted = false;
        }
        public object Clone()
        {
            return new Language
            {
                Id = Id,
                NameOfLanguage = NameOfLanguage,
                IsDeleted = IsDeleted

            };
        }
        public string Error
        {
            get
            {
              
                if (string.IsNullOrEmpty(NameOfLanguage))
                {
                    return "Name cannot be empty!";
                }
              

                return "";
            }
        }

        public string this[string columnName]
        {

            get
            {
                IsValid = true;
               
                if (columnName == "NameOfLanguage" && string.IsNullOrEmpty(NameOfLanguage))
                {
                    IsValid = false;
                    return "Name cannot be empty!";
                }
             

                return "";
            }
        }

        public override string ToString()
        {
            return $"{NameOfLanguage}";
        }
    }
}
