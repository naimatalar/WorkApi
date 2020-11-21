using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Work.Api.Helpers
{
    public static class Extensions
    {
        public static string FillModelStateError(this ModelStateDictionary  dic)
        {
            var data ="" ;
            foreach (var item in dic)
            {
                data = data + item.Value.ToString()+" **** ";
            }
            return "";
        }
    }
}
