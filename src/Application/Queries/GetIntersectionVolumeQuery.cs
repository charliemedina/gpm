namespace Application.Queries
{
    [DataContract]
    public class GetIntersectionVolumeQuery : IRequest<int>
    {
        [DataMember]
        public int ShapeId1 { get; set; }

        [DataMember]
        public int ShapeId2 { get; set; }

        public GetIntersectionVolumeQuery(int shapeId1, int shapeId2)
        {
            ShapeId1 = shapeId1;
            ShapeId2 = shapeId2;
        }
    }
}