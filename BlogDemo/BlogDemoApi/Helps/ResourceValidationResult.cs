using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogDemoApi.Helps
{
    /// <summary>
    /// 返回结果
    /// </summary>
    public class ResourceValidationResult : Dictionary<string, IEnumerable<ResourceValidationError>>
    {
        /// <summary>
        /// 不屈分大小写
        /// </summary>
        public ResourceValidationResult():base(StringComparer.OrdinalIgnoreCase)
        {

        }
        public ResourceValidationResult(ModelStateDictionary modelState):this()
        {
              if(modelState==null)
            {
                throw new ArgumentNullException(nameof(modelState));
            }
            foreach (var keymodelIStatePair in modelState)
            {
                var key = keymodelIStatePair.Key;
                var errors = keymodelIStatePair.Value.Errors;
                if(errors!=null&&errors.Count>0)
                {
                    var errorToAdd = new List<ResourceValidationError>();
                    foreach (var error in errors)
                    {
                        var keyAndMessage = error.ErrorMessage.Split('|');
                        if(keyAndMessage.Length>1)
                        {
                            errorToAdd.Add(new ResourceValidationError(keyAndMessage[1], keyAndMessage[0]));
                        }
                        else
                        {
                            errorToAdd.Add(new ResourceValidationError(keyAndMessage[0]));
                        }
                    }
                    Add(key, errorToAdd);
                }
            }
        }
    }
}
