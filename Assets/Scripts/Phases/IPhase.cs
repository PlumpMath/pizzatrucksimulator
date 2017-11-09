public interface IPhase {
    event System.Action OnPhaseFinish;

	void Begin();
	void End();
}