addEventListener("load", () => {
    document.querySelectorAll(".image-upload-area").forEach(el => {
        el.addEventListener("click", () => {
            const relatedElement = document.querySelector(el.getAttribute("data-related-input"));
            if (relatedElement) {
                relatedElement.click();
            }
            else {
                throw new Error("Cannot find related element");
            }
        });
    });
});