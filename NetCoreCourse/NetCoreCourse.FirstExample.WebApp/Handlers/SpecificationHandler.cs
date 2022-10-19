﻿using NetCoreCourse.FirstExample.WebApp.Dto;

namespace NetCoreCourse.FirstExample.WebApp.Handlers
{
    public class SpecificationHandler<T>
        //where T : Transport, new()
        //where T : Transport // Agreguemos esta restriccion.
    {
        public string GetSpecification(T element)
        {
            return $"Type: '{typeof(T)}' | ToString() '{element.ToString()}'";
        }

        //public T GetInstance()
        //{
        //    return new T();
        //}
    }
}
