namespace CakeOrder.Classes
{
    public class PageConstructor
    {
        public PageConstructor()
        {

        }

        public virtual void Open(int currentPosition) { }

        public void WriteLine(string text, int left = 0, int top = -1)
        {
            if (top == -1) 
                top = Console.GetCursorPosition().Top;
            Console.SetCursorPosition(left, top);
            Console.WriteLine(text);
        }
    }
}
