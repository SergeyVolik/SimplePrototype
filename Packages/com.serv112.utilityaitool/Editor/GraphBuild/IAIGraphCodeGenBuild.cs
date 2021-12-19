namespace SerV112.UtilityAIEditor
{
    public interface IAIGraphCodeGenBuild
    {
        AIGraphAssetModel Model { get; }
        void Build();
    }
}
