class WatermarkOptions {
    constructor(text) {
        this.text = text;
        this.background = "black";
        this.foreground = "white";
        this.fontSize = 14;
        this.positionX = "end";
        this.positionY = "end";
        this.padding = 4;
    }
}

function renderWatermark(canvas, options = new WatermarkOptions()) {
    console.log(options);
}