using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Enums
{
	public enum Score
	{
		Death = -150,
		Reflect = 5,
		Kill = 25,
		Coin = 100,
		NextLevel = 200,
	}

	public enum Sound
	{
		Death,
		Reflect,
		Kill,
		Coin,
		NextLevel,
	}

	public enum ObjectType
	{ 
		Coin,
		Slime
	}
}
