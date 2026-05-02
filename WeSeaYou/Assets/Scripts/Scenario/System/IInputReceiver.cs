public interface IInputReceiver
{
    // true を返すと「入力は自分が消費した、上には渡さないで」の意味
    // false を返すと「自分は何もしなかった、進行に使ってOK」
    bool HandleAdvanceInput();
}
