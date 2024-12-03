namespace CheckersGame
{
    public class Board
    {
        public char[,] Grid { get; private set; }
        public const char Empty = '.';
        public const char Player1Piece = 'o';
        public const char Player1King = 'K';
        public const char Player2Piece = 'x';
        public const char Player2King = 'K';

        public Board()
        {
            Grid = new char[8, 8];
            Initialize();
        }

        private void Initialize()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Grid[i, j] = Empty;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 == 1)
                        Grid[i, j] = Player2Piece;
                }
            }

            for (int i = 5; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 == 1)
                        Grid[i, j] = Player1Piece;
                }
            }
        }

        public void Print()
        {
            Console.WriteLine("  A B C D E F G H");
            for (int i = 0; i < 8; i++)
            {
                Console.Write((8 - i) + " ");
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(Grid[i, j] + " ");
                }
                Console.WriteLine((8 - i));
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public void MovePiece(int fromRow, int fromCol, int toRow, int toCol)
        {
            Grid[toRow, toCol] = Grid[fromRow, fromCol];
            Grid[fromRow, fromCol] = Empty;
        }

        public bool IsInBounds(int row, int col) => row >= 0 && row < 8 && col >= 0 && col < 8;
    }
}
