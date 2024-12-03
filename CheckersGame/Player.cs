namespace CheckersGame
{
    public class Player
    {
        public char Piece { get; }
        public char King { get; }
        public bool IsPlayer1 { get; }

        public Player(bool isPlayer1)
        {
            IsPlayer1 = isPlayer1;
            Piece = isPlayer1 ? Board.Player1Piece : Board.Player2Piece;
            King = isPlayer1 ? Board.Player1King : Board.Player2King;
        }

        public int Direction => IsPlayer1 ? -1 : 1; 
    }
}
