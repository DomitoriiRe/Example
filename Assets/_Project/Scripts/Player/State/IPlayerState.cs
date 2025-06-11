public interface IPlayerState
{
    PlayerMovementState CurrentPlayerMovementState { get; }
    void SetPlayerMovementState(PlayerMovementState playerMovementState); 
    bool InState();
}