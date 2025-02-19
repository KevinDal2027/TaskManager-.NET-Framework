const API_URL = "http://localhost:5075/tasks"; // Change this if needed

document.addEventListener("DOMContentLoaded", () => {
    const taskForm = document.getElementById("task-form");
    const taskName = document.getElementById("task-name");
    const taskCategory = document.getElementById("task-category");
    const taskPriority = document.getElementById("task-priority");
    const taskDueDate = document.getElementById("task-due-date");
    const taskList = document.getElementById("task-list");

    // Fetch and display tasks
    async function fetchTasks() {
        taskList.innerHTML = "";
        try {
            const response = await fetch(API_URL);
            const tasks = await response.json();

            // Sort by Due Date first, then by Priority
            tasks.sort((a, b) => {
                if (a.dueDate === b.dueDate) {
                    return a.priority - b.priority;
                }
                return new Date(a.dueDate) - new Date(b.dueDate);
            });

            tasks.forEach(task => addTaskToUI(task));
        } catch (error) {
            console.error("Error fetching tasks:", error);
        }
    }

    // Add a task to UI
    function addTaskToUI(task) {
        const taskItem = document.createElement("li");
        taskItem.classList.add("task");
        taskItem.dataset.id = task.id;

        taskItem.innerHTML = `
            <strong>${task.name}</strong><br>
            Category: ${task.categoryId === 1 ? "Academics" : "Others"}<br>
            Priority: ${task.priority}<br>
            Due Date: ${task.dueDate}
            <br>
            <button class="edit-btn" data-id="${task.id}">Edit</button>
            <button class="delete-btn" data-id="${task.id}">Delete</button>
        `;

        taskList.appendChild(taskItem);
    }

    // Handle form submission (Add Task)
    taskForm.addEventListener("submit", async (e) => {
        e.preventDefault();

        const newTask = {
            name: taskName.value.trim(),
            categoryId: Number(taskCategory.value),
            priority: Number(taskPriority.value),
            dueDate: taskDueDate.value
        };

        try {
            const response = await fetch(API_URL, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(newTask)
            });

            if (response.ok) {
                fetchTasks(); // Reload tasks
            }
        } catch (error) {
            console.error("Error adding task:", error);
        }

        // Clear form
        taskForm.reset();
    });

    // Edit a task
    taskList.addEventListener("click", async (event) => {
        if (event.target.classList.contains("edit-btn")) {
            const id = event.target.dataset.id;
            const newName = prompt("Enter new task name:");
            const newPriority = prompt("Enter new priority (1-100):");
            const newDueDate = prompt("Enter new due date (YYYY-MM-DD):");

            if (!newName || !newPriority || !newDueDate) return;

            const updatedTask = {
                name: newName,
                priority: Number(newPriority),
                dueDate: newDueDate
            };

            try {
                const response = await fetch(`${API_URL}/${id}`, {
                    method: "PUT",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify(updatedTask)
                });

                if (response.ok) {
                    fetchTasks(); // Reload tasks
                }
            } catch (error) {
                console.error("Error updating task:", error);
            }
        }
    });

    // Delete a task
    taskList.addEventListener("click", async (event) => {
        if (event.target.classList.contains("delete-btn")) {
            const id = event.target.dataset.id;
            if (!confirm("Are you sure you want to delete this task?")) return;

            try {
                const response = await fetch(`${API_URL}/${id}`, { method: "DELETE" });

                if (response.ok) {
                    fetchTasks(); // Reload tasks
                }
            } catch (error) {
                console.error("Error deleting task:", error);
            }
        }
    });

    fetchTasks(); // Load tasks on page load
});
