// Learn TypeScript:
//  - https://docs.cocos.com/creator/manual/en/scripting/typescript.html
// Learn Attribute:
//  - https://docs.cocos.com/creator/manual/en/scripting/reference/attributes.html
// Learn life-cycle callbacks:
//  - https://docs.cocos.com/creator/manual/en/scripting/life-cycle-callbacks.html

import ItemController from "./ItemController";

const {ccclass, property} = cc._decorator;

@ccclass
export default class ReelController extends cc.Component {

    private nodeItems: cc.Node[] = [];

    private minHeightPosition: number = -405;
    private maxHeightPosition: number = 405;

    private heightItem: number = 135;

    // trạng thái reel đang chạy
    private isRunning: boolean = false;
    private isChangeResult: boolean = false;

    public typeSpin: number = 0; //0 là spinDown, 1 là spinUp,
    public spinSpeed: number = 27; // Tốc độ quay của reel, phải là ước của 135

    public nodeResult1: Node;
    public nodeResult2: Node;
    public nodeResult3: Node;

    private result: number[] = [1,2,3,4,5,6,7];

    start() {
        this.nodeItems = this.node.children;
    }

    update(deltaTime: number) {
        if (this.isRunning == false) {
            // dừng reel tại điểm mặc định
            this.changeResult();
        }
        else {
            this.spin();
        }

    }

    private spin() {

        if (this.typeSpin == 0) {
            this.spinDown();
        }
        else {
            this.spinUp();
        }
    }

    private spinUp() {
        this.nodeItems.forEach((node, index) => {

            let position = node.getPosition();
            node.setPosition(position.x, position.y + this.spinSpeed);

        });

        let lastNode: cc.Node;
        let arrPositionY: number[] = this.nodeItems.map(elm => elm.getPosition().y);
        lastNode = this.nodeItems.find(elm => elm.getPosition().y === Math.max(...arrPositionY));

        let positionLastNode = lastNode.getPosition();

        if (positionLastNode.y == this.maxHeightPosition + this.heightItem) {

            lastNode.setPosition(positionLastNode.x, this.minHeightPosition);
        }
    }
    private spinDown() {
        this.nodeItems.forEach((node, index) => {

            let position = node.getPosition();
            node.setPosition(position.x, position.y - this.spinSpeed);

        });
        let lastNode: cc.Node;
        let arrPositionY: number[] = this.nodeItems.map(elm => elm.getPosition().y);
        lastNode = this.nodeItems.find(elm => elm.getPosition().y === Math.min(...arrPositionY));

        let positionLastNode = lastNode.getPosition();

        if (positionLastNode.y == this.minHeightPosition - this.heightItem) {

            lastNode.setPosition(positionLastNode.x, this.maxHeightPosition);
        }
    }


    public startReel() {

        if (this.isRunning == false) {
            this.isRunning = true;
        }

    }
    public stopReel() {
        if (this.isRunning == true) {
            this.isRunning = false;
        }
    }
    //result là mảng id kết quả của reel đó
    public setResult(result: number[]) {
        this.result = result;
        this.isChangeResult = true;
    }
    public changeResult(){
        if(this.isChangeResult){
            for (let index = 0; index < this.result.length; index++) {
                this.nodeItems.find(e => e.getComponent('ItemController').Id == this.result[index]).setPosition(0, this.maxHeightPosition - this.heightItem * index);
            }
            this.isChangeResult = false;
        }
    }


    public getValues() {
        let arr: number[] = [];
        arr.push(this.nodeItems.find(e => e.getPosition().y == 135).getComponent('ItemController').Id);
        arr.push(this.nodeItems.find(e => e.getPosition().y == 0).getComponent('ItemController').Id);
        arr.push(this.nodeItems.find(e => e.getPosition().y == -135).getComponent('ItemController').Id);

        return arr;
    }
}
