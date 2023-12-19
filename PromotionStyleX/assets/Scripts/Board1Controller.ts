// Learn TypeScript:
//  - https://docs.cocos.com/creator/manual/en/scripting/typescript.html
// Learn Attribute:
//  - https://docs.cocos.com/creator/manual/en/scripting/reference/attributes.html
// Learn life-cycle callbacks:
//  - https://docs.cocos.com/creator/manual/en/scripting/life-cycle-callbacks.html

import ReelController from "./ReelController";
const { ccclass, property } = cc._decorator;

@ccclass
export default class NewClass extends cc.Component {

    @property(cc.Prefab)
    public reelPrefab: cc.Prefab | null = null;

    public reel1Script: ReelController;
    public reel2Script: ReelController;
    public reel3Script: ReelController;

    start() {
        let reel1 = cc.instantiate(this.reelPrefab);
        let reel2 = cc.instantiate(this.reelPrefab);
        let reel3 = cc.instantiate(this.reelPrefab);

        this.node.addChild(reel1);
        this.node.addChild(reel2);
        this.node.addChild(reel3);

        reel1.setPosition(new cc.Vec2(-145, 0));
        reel2.setPosition(new cc.Vec2(0, 0));
        reel3.setPosition(new cc.Vec2(145, 0));


        this.reel1Script = reel1.getComponent('ReelController');
        this.reel2Script = reel2.getComponent('ReelController');
        this.reel3Script = reel3.getComponent('ReelController');

        this.reel2Script.typeSpin = 1; //spinUp

        // this.node.on(cc.Node.EventType.MOUSE_DOWN, (event) => {
        //     this.handleClick(event);
        // },this);
    }

    startReel(){
        this.reel1Script.startReel();
        this.reel2Script.startReel();
        this.reel3Script.startReel();
    }
    setResult(result1: number[], result2: number[],result3: number[]){
        this.reel1Script.setResult(result1);
        this.reel2Script.setResult(result2);
        this.reel3Script.setResult(result3);
    }

    stopReel(){
        this.reel1Script.stopReel();
        this.reel2Script.stopReel();
        this.reel3Script.stopReel();
    }

    update(deltaTime: number) {

    }


}
