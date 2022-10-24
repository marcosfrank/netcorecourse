namespace NetCoreCourse.FirstExample.WebApp.Entities
{
    public abstract class EntidadBase
    {
        public int Edad { get; set; }
        public bool EsMayorDeEdad {
            get
            { 
                return Edad > 18;    
            }
        }
        private int _id;
        public int Id 
        {
            get
            {
                return _id;
            }
            set
            {
                if(_id != value)
                    _id = value; 
            } 
        }
        public string Descripcion { get; set; }
    }
}
