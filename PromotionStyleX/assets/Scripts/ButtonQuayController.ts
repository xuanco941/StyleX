// Learn TypeScript:
//  - https://docs.cocos.com/creator/manual/en/scripting/typescript.html
// Learn Attribute:
//  - https://docs.cocos.com/creator/manual/en/scripting/reference/attributes.html
// Learn life-cycle callbacks:
//  - https://docs.cocos.com/creator/manual/en/scripting/life-cycle-callbacks.html

const { ccclass, property } = cc._decorator;

@ccclass
export default class ButtonQuayController extends cc.Component {
    @property(cc.SpriteFrame)
    SpriteON: cc.SpriteFrame = null;
    @property(cc.SpriteFrame)
    SpriteOFF: cc.SpriteFrame = null;


    board1: cc.Node;
    soLuotQuay: cc.Node;
    isQuay: boolean = false;
    protected onLoad(): void {
        this.board1 = cc.find('Canvas/Main Board/Mask 1/Board');
        this.soLuotQuay = cc.find('Canvas/Main Board/board_number/soLuotQuay');
    }

    start() {
        var clickEventHandler = new cc.Component.EventHandler();
        // This node is the node to which your event handler code component belongs
        clickEventHandler.target = this.node;
        // This is the script class name
        clickEventHandler.component = 'ButtonQuayController';
        clickEventHandler.handler = 'handleButtonQuay';
        clickEventHandler.customEventData = '';

        const button = this.node.getComponent(cc.Button);
        button.clickEvents.push(clickEventHandler);

    }

    handleButtonQuay() {
        if (!this.isQuay) {
            this.soLuotQuay.getComponent(cc.Label).string = "Còn 4 lượt";
            this.node.getComponent(cc.Sprite).spriteFrame = this.SpriteOFF;
            this.board1.getComponent('Board1Controller').startReel();

            this.isQuay = true;
        }
        else {
            this.node.getComponent(cc.Sprite).spriteFrame = this.SpriteON;
            this.board1.getComponent('Board1Controller').setResult(this.generateRandomArray(), this.generateRandomArray(), this.generateRandomArray());
            this.board1.getComponent('Board1Controller').stopReel();
            this.isQuay = false;
        }
    }
    //tạo kết quả ngẫu nhiên
    generateRandomArray() {
        let result: number[] = [];

        while (result.length < 7) {
            const randomNumber = Math.floor(Math.random() * (7)) + 1;

            // Kiểm tra số ngẫu nhiên đã tồn tại trong mảng chưa
            if (result.indexOf(randomNumber) == -1) {
                result.push(randomNumber);
            }
        }

        return result;
    }
}
