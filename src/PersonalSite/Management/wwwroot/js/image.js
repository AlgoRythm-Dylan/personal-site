const DEFAULT_MAX_IMAGE_SIZE = 450;

async function getPaletteFromFile(file, maxImageSize = null) {
    const image = await createImageFromFile(file);
    const reducedImage = shrinkImage(image, maxImageSize ?? DEFAULT_MAX_IMAGE_SIZE);
    document.body.appendChild(reducedImage);
}

// Returns an image object from a file object
function createImageFromFile(file) {
    // Partial credit (modified from):
    // https://stackoverflow.com/questions/7460272/getting-image-dimensions-using-the-javascript-file-api
    return new Promise((res, rej) => {
        let reader = new FileReader();

        reader.onload = () => {
            let img = new Image();
            img.onload = () => res(img);
            img.src = reader.result;
        }

        reader.readAsDataURL(file);
    });
}


// Using an image object, creates a canvas where the max
// dimension is at most `maxImageSize` or less, if the
// input is less
function shrinkImage(image, maxImageSize = null) {
    const naturalWidth = image.naturalWidth;
    const naturalHeight = image.naturalHeight;
    var outputWidth, outputHeight;
    let canvas = document.createElement("canvas");
    if (Math.max(naturalHeight, naturalWidth) <= maxImageSize) {
        // Image can be used as-is, just create the canvas and return
        outputWidth = naturalWidth;
        outputHeight = naturalHeight;
    }
    else {
        // In this branch, one or both of the dimensions need to be reduced.
        // We can simply reduce the largest dimension and scale the other
        // proportionally
        if (naturalWidth > naturalHeight) {
            outputWidth = maxImageSize;
            outputHeight = Math.floor(naturalHeight * (maxImageSize / naturalWidth));
        }
        else {
            outputHeight = maxImageSize;
            outputWidth = Math.floor(naturalWidth * (maxImageSize / naturalHeight));
        }
    }
    canvas.width = outputWidth;
    canvas.height = outputHeight;
    let ctx = canvas.getContext("2d");
    ctx.drawImage(image, 0, 0, outputWidth, outputHeight);
    return canvas;
}