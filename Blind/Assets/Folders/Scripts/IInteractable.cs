public interface IInteractable
{
    public string interactTxt { get; }
    public void OnLookedAt(PlayerInteractor plrInteractor);
    public void OnInteractedWith(PlayerInteractor plrInteractor);
}
