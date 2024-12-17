namespace AdventOfCode.Common;
public enum Directions
{
    TOP,
    DOWN,
    RIGHT,
    LEFT,
    TOPLEFT,
    TOPRIGHT,
    DOWNLEFT,
    DOWNRIGHT
}

public static class DirectionsExtensions
{
    public static (int x, int y) GetDirectionCoordinates(this Directions direction)
    {
        return direction switch
        {
            Directions.TOP => (-1, 0),
            Directions.DOWN => (1, 0),
            Directions.LEFT => (0, -1),
            Directions.RIGHT => (0, 1),
            Directions.TOPLEFT => (-1, -1),
            Directions.TOPRIGHT => (-1, 1),
            Directions.DOWNLEFT => (1, -1),
            Directions.DOWNRIGHT => (1, 1),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    public static Directions TurnRight(this Directions direction)
    {
        return direction switch
        {
            Directions.TOP => Directions.RIGHT,
            Directions.DOWN => Directions.LEFT,
            Directions.LEFT => Directions.TOP,
            Directions.RIGHT => Directions.DOWN,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    public static Directions TurnLeft(this Directions direction)
    {
        return direction switch
        {
            Directions.TOP => Directions.LEFT,
            Directions.DOWN => Directions.RIGHT,
            Directions.LEFT => Directions.DOWN,
            Directions.RIGHT => Directions.TOP,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
}