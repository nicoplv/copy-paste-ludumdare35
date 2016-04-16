namespace Utils
{
	public class Tuple<T1, T2>
	{
		#region Variables

		public T1 Item1 { get; protected set; }
		public T2 Item2 { get; protected set; }

		#endregion

		#region Constructor

		public Tuple(T1 _item1, T2 _item2)
		{
			Item1 = _item1;
			Item2 = _item2;
		}

		#endregion
	}

	public class Tuple<T1, T2, T3>
	{
		#region Variables

		public T1 Item1 { get; protected set; }
		public T2 Item2 { get; protected set; }
		public T3 Item3 { get; protected set; }

		#endregion

		#region Constructor

		public Tuple(T1 _item1, T2 _item2, T3 _item3)
		{
			Item1 = _item1;
			Item2 = _item2;
			Item3 = _item3;
		}

		#endregion
	}

	public class Tuple<T1, T2, T3, T4>
	{
		#region Variables

		public T1 Item1 { get; protected set; }
		public T2 Item2 { get; protected set; }
		public T3 Item3 { get; protected set; }
		public T4 Item4 { get; protected set; }

		#endregion

		#region Constructor

		public Tuple(T1 _item1, T2 _item2, T3 _item3, T4 _item4)
		{
			Item1 = _item1;
			Item2 = _item2;
			Item3 = _item3;
			Item4 = _item4;
		}

		#endregion
	}
}