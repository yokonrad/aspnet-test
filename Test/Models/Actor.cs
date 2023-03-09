namespace Test.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ActorMovie> ActorMovies { get; set; } = new List<ActorMovie>();
    }
}
