using System;

//链表结点
public class Node<T>
{
	public Node<T> Next { get; set; }
	public T Data { get; set; }
	public Node(T t)
	{
		Next = null;
		Data = t;
	}
}
