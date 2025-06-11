namespace Controller
{ 
    public class PlayerState : IPlayerState
    {
        public PlayerMovementState CurrentPlayerMovementState { get; private set; } = PlayerMovementState.Idling; 
        public void SetPlayerMovementState(PlayerMovementState playerMovementState) => CurrentPlayerMovementState = playerMovementState; 

        public bool InState()
        {
            return IsState(CurrentPlayerMovementState);
        }

        private bool IsState(PlayerMovementState movementState)
        {
            return movementState == PlayerMovementState.Idling || movementState == PlayerMovementState.Running;
        }
    }
} 