﻿using System.Text.Json;

namespace API_Layer.Exceptions
{
    public class ErrorModel
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
