﻿namespace GameStore.DAL.Entities
{
    public class GameGenre
    {
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
