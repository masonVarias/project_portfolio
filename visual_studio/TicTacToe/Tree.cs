using System;

public class Tree
{
    Leaf head;
	public Tree()
	{
        Leaf head = new Leaf();
	}


    public class Leaf
    {
        int x;
        int y;
        int val;
        LinkedList<Leaf> children;

        public Leaf(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.val = 0;
            children = new LinkedList<Leaf>();
        }

        public void AddChild(Leaf passed)
        {
            children.AddLast(passed);
        }
    }
}
