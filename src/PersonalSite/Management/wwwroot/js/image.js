const DEFAULT_MAX_IMAGE_SIZE = 450;
const DEFAULT_SPLITS = 3;
const DEFAULT_SIMILARITY_THRESHOLD = 0.05; // 5%

class ImageInfo {
    constructor() {
        this.palette = null;
        this.brightness = null;
        this.image = null;
    }
}

async function getImageInfoFromFile(file, maxImageSize = DEFAULT_MAX_IMAGE_SIZE) {
    let info = new ImageInfo();

    // Image preprocessing
    const image = await createImageFromFile(file);
    const reducedImage = shrinkImage(image, maxImageSize);
    const pixelData = getPixelData(reducedImage);

    // Image processing
    info.palette = getPalette(pixelData);
    removeDuplicateColors(info.palette);
    info.brightness = getBrightness(pixelData);

    info.image = image;

    return info;
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

function getPixelData(canvas) {
    let data = new Array(canvas.width * canvas.height);
    let ctx = canvas.getContext("2d");
    let pixels = ctx.getImageData(0, 0, canvas.width, canvas.height);
    for (let i = 0; i < pixels.data.length; i += 4) {
        data[i / 4] = [pixels.data[i], pixels.data[i + 1], pixels.data[i + 2]];
    }
    return data;
}

const COLOR_CHANNEL = {
    Red: 0,
    Green: 1,
    Blue: 2
};

function getChannelWithLargestRange(pixelData) {
    const redRange = getChannelRange(pixelData, COLOR_CHANNEL.Red);
    const blueRange = getChannelRange(pixelData, COLOR_CHANNEL.Blue);
    const greenRange = getChannelRange(pixelData, COLOR_CHANNEL.Green);

    const maxRange = Math.max(redRange, greenRange, blueRange);

    if (maxRange === redRange) {
        return COLOR_CHANNEL.Red;
    }
    else if (maxRange === greenRange) {
        return COLOR_CHANNEL.Green;
    }
    else {
        return COLOR_CHANNEL.Blue;
    }
}

function getChannelRange(pixelData, channel) {
    let min = pixelData[0][channel];
    let max = pixelData[0][channel];
    for (let i = 1; i < pixelData.length; i++) {
        min = Math.min(min, pixelData[i][channel]);
        max = Math.max(max, pixelData[i][channel]);
    }
    return max - min;
}

function sortListByChannel(list, channel) {
    list.sort((a, b) => a[channel] - b[channel]);
}

function splitListInHalf(list) {
    const middle = Math.ceil(list.length / 2);
    const firstHalf = list.slice(0, middle);
    const secondHalf = list.slice(middle);
    return [firstHalf, secondHalf];
}

function getAverageColor(list) {
    if (list.length === 0) return [0, 0, 0]; // Handle empty list case

    let sumR = 0, sumG = 0, sumB = 0;

    for (let i = 0; i < list.length; i++) {
        sumR += list[i][COLOR_CHANNEL.Red];
        sumG += list[i][COLOR_CHANNEL.Green];
        sumB += list[i][COLOR_CHANNEL.Blue];
    }

    return [
        Math.round(sumR / list.length),
        Math.round(sumG / list.length),
        Math.round(sumB / list.length)
    ];
}

function getBrightness(pixelData) {
    return getAverageColor(pixelData).reduce((prev, current) => prev + current) / 3 / 255;
}

function getPalette(pixelData, splits = DEFAULT_SPLITS) {
    let results = [pixelData];
    for (let i = 0; i < splits; i++) {
        let splitResults = [];
        for (let result of results) {
            const maxRangeChannel = getChannelWithLargestRange(result);
            sortListByChannel(result, maxRangeChannel);
            splitResults.push(...splitListInHalf(result));
        }
        results = splitResults;
    }

    let averagedResults = [];

    for (let result of results) {
        averagedResults.push(getAverageColor(result));
    }

    return averagedResults;
}

function colorDistance(color1, color2) {
    return Math.sqrt(
        Math.pow(color1[COLOR_CHANNEL.Red] - color2[COLOR_CHANNEL.Red], 2) +
        Math.pow(color1[COLOR_CHANNEL.Green] - color2[COLOR_CHANNEL.Green], 2) +
        Math.pow(color1[COLOR_CHANNEL.Blue] - color2[COLOR_CHANNEL.Blue], 2)
    );
}

const MAX_COLOR_DISTANCE = colorDistance([0, 0, 0], [255, 255, 255]); // Approx 441.6

// If similarityThreshold is 0.05, this removes any colors in the list
// that are within 5% similarity of each other.
function removeDuplicateColors(list, similarityThreshold = DEFAULT_SIMILARITY_THRESHOLD) {
    // Explosive complexity? Yes. Not designed for lists of long
    // length
    for (let i = 0; i < list.length; i++) {
        for (let j = 0; j < list.length; j++) {
            if (i === j) {
                // Skip comparing a color to itself
                continue;
            }
            const color1 = list[i];
            const color2 = list[j];
            const similarity = 1 - (colorDistance(color1, color2) / MAX_COLOR_DISTANCE);
            if (similarity > (1 - similarityThreshold)) {
                list.splice(j, 1);
                // Removing an item from the list makes it 1 element
                // shorter

                // Adjust the array "cursor" of the outer loop
                // if it is beyond the element we just removed
                if (i >= j) {
                    i--;
                }
                // Always need to adjust the inner loop "cursor" position
                j--;
            }
        }
    }
}