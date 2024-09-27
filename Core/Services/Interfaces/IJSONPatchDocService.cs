using Core.DTOs.Collections;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IJSONPatchDocService<T> where T : class
    {
        public JsonPatchDocument<T> ConvertPatchDocToEntity(
            JsonPatchDocument<dynamic> patchDoc);




    }
}
