using System;

namespace CheckersGame
{
    public class Game
    {
        private Board board;
        private Player player1;
        private Player player2;
        private bool isPlayer1Turn;

        public Game()
        {
            board = new Board();
            player1 = new Player(true);
            player2 = new Player(false);
            isPlayer1Turn = true;
        }

      public void Start()
{
    while (true)
    {
        Console.Clear();
        board.Print();

        
        if (CheckGameOver())
        {
            Console.WriteLine(isPlayer1Turn ? "Player 2 Wins!" : "Player 1 Wins!");
            break;
        }

        Player currentPlayer = isPlayer1Turn ? player1 : player2;
        Console.WriteLine(isPlayer1Turn ? "Player 1's Turn (o):" : "Player 2's Turn (x):");
        Console.Write("Enter move (e.g., A3-B4): ");
        string move = Console.ReadLine();

        if (ProcessMove(move, currentPlayer))
        {
            isPlayer1Turn = !isPlayer1Turn;
        }
        else
        {
            Console.WriteLine("Invalid move. Press Enter to try again.");
            Console.ReadLine();
        }
    }
}



        private bool ProcessMove(string move, Player player)
        {
            if (string.IsNullOrWhiteSpace(move) || move.Length != 5 || move[2] != '-')
                return false;

            int fromRow = 8 - (move[1] - '0');
            int fromCol = move[0] - 'A';
            int toRow = 8 - (move[4] - '0');
            int toCol = move[3] - 'A';

            if (IsValidMove(fromRow, fromCol, toRow, toCol, player))
            {
                board.MovePiece(fromRow, fromCol, toRow, toCol);

                //? Promote piece if it reaches the end of the board
                if (toRow == 0 && board.Grid[toRow, toCol] == player1.Piece)
                    board.Grid[toRow, toCol] = player1.King;

                if (toRow == 7 && board.Grid[toRow, toCol] == player2.Piece)
                    board.Grid[toRow, toCol] = player2.King;

                return true;
            }

            return false;
        }

  private bool IsValidMove(int fromRow, int fromCol, int toRow, int toCol, Player player)
{
    if (!board.IsInBounds(fromRow, fromCol) || !board.IsInBounds(toRow, toCol))
        return false;

    char piece = board.Grid[fromRow, fromCol];
    char target = board.Grid[toRow, toCol];

    
    if (piece != player.Piece && piece != player.King)
        return false;

    
    if (target != Board.Empty)
        return false;

    int direction = player.Direction;
    bool isKing = piece == player.King;

   
    if (Math.Abs(toCol - fromCol) == 1 && (toRow - fromRow == direction || isKing))
        return true;

    
    if (Math.Abs(toCol - fromCol) == 2 && Math.Abs(toRow - fromRow) == 2)
    {
        int jumpRow = (fromRow + toRow) / 2;
        int jumpCol = (fromCol + toCol) / 2;

        char opponentPiece = player.IsPlayer1 ? Board.Player2Piece : Board.Player1Piece;
        char opponentKing = player.IsPlayer1 ? Board.Player2King : Board.Player1King;

        
        if (board.Grid[jumpRow, jumpCol] == opponentPiece || board.Grid[jumpRow, jumpCol] == opponentKing)
        {
            board.Grid[jumpRow, jumpCol] = Board.Empty; 
            return true;
        }
    }

    return false;
}


 private bool CheckGameOver()
{
    bool player1HasPieces = false, player1HasMoves = false;
    bool player2HasPieces = false, player2HasMoves = false;

    for (int i = 0; i < 8; i++)
    {
        for (int j = 0; j < 8; j++)
        {
            char piece = board.Grid[i, j];

            if (piece == Board.Player1Piece || piece == Board.Player1King)
            {
                player1HasPieces = true;
                if (HasValidMoves(i, j, player1))
                    player1HasMoves = true;
            }
            else if (piece == Board.Player2Piece || piece == Board.Player2King)
            {
                player2HasPieces = true;
                if (HasValidMoves(i, j, player2))
                    player2HasMoves = true;
            }
        }
    }

    
    return !(player1HasPieces && player1HasMoves && player2HasPieces && player2HasMoves);
}


        private bool HasValidMoves(int row, int col, Player player)
        {
            int[] directions = { -1, 1 };

            foreach (int dRow in directions)
            {
                foreach (int dCol in directions)
                {
                    int toRow = row + dRow;
                    int toCol = col + dCol;

                    if (IsValidMove(row, col, toRow, toCol, player))
                        return true;
                }
            }

            return false;
        }
    }
}
