package psgg;

class HxUTF8Encoding extends HxEncoding {

    public function new(bom:Bool){
        super();
        this.enc = "utf8";
        this.bom = bom;
    }

}