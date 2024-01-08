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
    trungThuong: cc.Node;
    labelTrungThuong: cc.Node;
    mayManLanSau: cc.Node;


    numberQuay: number = 0;
    protected onLoad(): void {
        this.board1 = cc.find('Canvas/Main Board/Mask 1/Board');
        this.soLuotQuay = cc.find('Canvas/Main Board/board_number/soLuotQuay');
        this.trungThuong = cc.find('Canvas/TrungThuong');
        this.labelTrungThuong = cc.find('Canvas/TrungThuong/LabelTrungThuong');
        this.mayManLanSau = cc.find('Canvas/MayManLanSau');

        this.trungThuong.active = false;
        this.mayManLanSau.active = false;

    }

    start() {
        this.board1.getComponent('Board1Controller').setResult(this.generateRandomArray(), this.generateRandomArray(), this.generateRandomArray());

        var clickEventHandler = new cc.Component.EventHandler();
        // This node is the node to which your event handler code component belongs
        clickEventHandler.target = this.node;
        // This is the script class name
        clickEventHandler.component = 'ButtonQuayController';
        clickEventHandler.handler = 'handleButtonQuay';
        clickEventHandler.customEventData = '';

        const button = this.node.getComponent(cc.Button);
        button.clickEvents.push(clickEventHandler);

        this.getNumberPlay();
    
    }

    async handleButtonQuay() {
        if (!this.isQuay) {
            this.soLuotQuay.getComponent(cc.Label).string = `Còn ${this.numberQuay-1} lượt`;
            this.node.getComponent(cc.Sprite).spriteFrame = this.SpriteOFF;
            this.board1.getComponent('Board1Controller').startReel();

            this.isQuay = true;
        }
        else {
            this.node.getComponent(cc.Sprite).spriteFrame = this.SpriteON;
            await this.getResult();
            await this.getNumberPlay();
        }
    }

    notifySpin(numberSale){
        if(numberSale>0){
            cc.find('Canvas/TrungThuong/LabelTrungThuong').getComponent(cc.RichText).string= `<color=yellow >Chúc mừng bạn nhận được 1 phiếu giảm giá ${numberSale}%</color>`;
            cc.find('Canvas/TrungThuong').active = true;
            var timeoutID = setTimeout(function() {
                cc.find('Canvas/TrungThuong').active = false;
                // Hủy bỏ timeout ngay sau khi hàm được thực hiện
                clearTimeout(timeoutID);
            }, 4000);
        }
        else{
            cc.find('Canvas/MayManLanSau').getComponent(cc.RichText).string= `<color=green>Chúc bạn may mắn lần sau!</color>`;
            cc.find('Canvas/MayManLanSau').active = true;                        

            var timeoutID = setTimeout(function() {
                cc.find('Canvas/MayManLanSau').active = false;
                // Hủy bỏ timeout ngay sau khi hàm được thực hiện
                clearTimeout(timeoutID);
            }, 4000);
        }

    }

    async getResult() {
        await fetch('/Promotion/GetResult', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
            })
        })
            .then(response => response.json())
            .then(data => {
                if (data.status == 1) {
                    this.board1.getComponent('Board1Controller').setResult(data.result1, data.result2, data.result3);
                    this.notifySpin(data.numberSale);
                }
                else {
                    cc.find('Canvas/MayManLanSau').getComponent(cc.RichText).string= `<color=red>Có lỗi xảy ra, vui lòng thử lại sau!</color>`;
                    cc.find('Canvas/MayManLanSau').active = true;    
                    var timeoutID = setTimeout(function() {
                        cc.find('Canvas/MayManLanSau').active = false;                    
                        // Hủy bỏ timeout ngay sau khi hàm được thực hiện
                        clearTimeout(timeoutID);
                    }, 4000);
                }
                this.board1.getComponent('Board1Controller').stopReel();
                this.isQuay = false;
            })
            .catch(error => {
                cc.find('Canvas/MayManLanSau').getComponent(cc.RichText).string= `<color=red>Có lỗi xảy ra, vui lòng thử lại sau!</color>`;
                cc.find('Canvas/MayManLanSau').active = true;    
                var timeoutID = setTimeout(function() {
                    cc.find('Canvas/MayManLanSau').active = false;                    
                    // Hủy bỏ timeout ngay sau khi hàm được thực hiện
                    clearTimeout(timeoutID);
                }, 4000);
            });
    }

    async getNumberPlay() {
        await fetch('/Promotion/GetNumberPlay', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
            })
        })
            .then(response => response.json())
            .then(data => {
                this.numberQuay = data.number;
                this.soLuotQuay.getComponent(cc.Label).string = `Còn ${data.number} lượt`;
                if(data.number>0){
                    this.node.active = true;
                }
                else{
                    this.node.active = false;
                }
            })
            .catch(error => {
                this.soLuotQuay.getComponent(cc.Label).string = `Hệ thống bận`;
                this.node.active = false;
            });
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
