namespace parcialherramientas.Models;

public class Actor
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int Age { get; set; }
    
    public string Country { get; set;}

    public int? MovieId { get; set; }

    //public virtual ICollection<Movie> Movies { get; set; }
}
