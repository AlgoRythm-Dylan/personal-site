class Task {
    constructor(name="[unnamed job]", description=null) {
        this.name = name;
        this.description = description;
        this.data = {};
    }
    async execute() { } // "Virtual" function
}

class Job {
    constructor() {
        this.tasks = [];
    }
    async run() {
        for (const task of this.tasks) {
            await task.execute();
        }
    }
}

class CreateNewImageTask extends Task {
    constructor() {
        this.super("Create new image", "Creating a new image object in the database");

        this.title = null;
        this.description = null;
        this.noAlterations = true;
        this.alterationsDescription = null;
        this.sourceWidth = null;
        this.sourceHeight = null;
    }
}