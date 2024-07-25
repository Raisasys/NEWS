using Core;

namespace Domain;

public record Status(RAG? RAG, Direction? Direction)
{
    public Status() : this(null,null) { }

    public RAG? RAG{ get; set; } = RAG;
    public Direction? Direction { get; set; } = Direction;
}