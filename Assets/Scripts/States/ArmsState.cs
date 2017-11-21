public abstract class ArmsState {
    protected Arms arms;

    public abstract void Tick();

    public virtual void OnEnter() { }
    public virtual void OnExit() { }

    public ArmsState(Arms arms) {
        this.arms = arms;
    }
}