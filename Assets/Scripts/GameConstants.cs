

namespace Unity.FPS.Game
{
    public class GameConstants
    {

        public const string k_AxisNameVertical = "Vertical";
        public const string k_AxisNameHorizontal = "Horizontal";
        public const string k_MouseAxisNameVertical = "Mouse Y";
        public const string k_MouseAxisNameHorizontal = "Mouse X";

        public const string k_ButtonNameAim = "Aim";
        public const string k_ButtonNameFire = "Fire";
        public const string k_ButtonNameSubmit = "Submit";
        public const string k_ButtonNamePickupLeft = "PickupLeft";
        public const string k_ButtonNameThrowLeft = "ThrowLeft";
        public const string k_ButtonNamePauseMenu = "Pause Menu";

    }

}
//Tag：被捡起的物体 Layer：被攻击的物体 A型无法被捡起来。B型需要被捡起来，扔到墙上消失。C型需要被捡起来，扔出去不会消失。
//弹球：Tag：canPickUp Layer：enemy
//木门：Tag：None Layer：enemy
//钥匙门：Tag：None Layer：Keyenemy
//书，碎石，花瓶，小平井盖 ：Tag：canPickUp Layer：enemy
//矮脚桌，箱子：Tag：None Layer：None
//普通绿色小怪：Tag：EnemycanPickUp Layer：enemy
//紫色飞行小怪：Tag：None Layer：enemy
//白色射击小怪：Tag：None Layer：enemy

//普通不能被抓起的物体：矮脚桌，箱子

//A型物体无法被捡起来
//B型物体需要被捡起来，扔到墙上消失
//C型物体需要被捡起来，扔出去不会消失

//Enemycan PickUp
//canPickUp


//弹球：
////实墙：只能被球反弹
////地面：被球反弹
//木门：被球攻击击碎
//钥匙门：只能被钥匙击碎
//普通被抓起的物体：（书，碎石，花瓶，小平井盖）
//普通不能被抓起的物体：矮脚桌，箱子，
//普通小怪：可以被抓起，扔出去, 击中任何物体，死去
//飞行小怪：不可以被抓起，扔出去，被击中，死去
//射击小怪：不可以被抓起

//钥匙就是DefaultLayer，普通击中就被销毁
//钥匙门是其他Layer，被钥匙击中才能被销毁，钥匙门需要加OnCollisionEnter