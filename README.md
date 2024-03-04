# blueprint-hub

# Short descrition
This repository stores examples of various implementations based on the same business requirements. The repository was created as an example of different ways to solve the same problem. 
None of the presented solutions is considered perfect. 
Each solution presents certain advantages and disadvantages. 
Whether a given architecture is appropriate for a specific problem always depends on the context of the decisions being made. I hope that this repository will help to show a broader picture of different kinds of approaches.

# Introduction to business requirements
Our repository will implement a certain set of business requirements. In the presented examples, we want to show both simpler and more complex use cases. For this reason, our business domain must possess a certain level of complexity. On the other hand, we obviously do not want the complexity of the presented domain to hinder the understanding of the examples. 
For this reason, we have chosen a domain that will probably be at least partially familiar to the reader - it will be the sphere of higher education institutions' functioning. Of course, for the purposes of our examples, we will simplify the real business processes a bit and focus only on a few of them. It will therefore be an excerpt of a somewhat simplified reality, which I hope will be enough to present both simpler and more complex implementation examples.

# Business requirements

## Context
Our application will handle some of the processes necessary for the operation of a higher education institution. 
We assume that our system is to be introduced at a private university. 
The mentioned university educates approximately 10,000 students annually. 
The university currently offers several different fields of study across several faculties. 

The operation of the university requires the cooperation of many different departments, including: 
- recruitment department - responsible for interviewing candidates for studies 
- dean's office - responsible for processes related to student services, educators, grades, current student affairs, and promotions for the new semester 
- accounting department - handling student payments and employee salaries 
- administration department - managing room service and equipment management 
- IT department - providing technical support for computers located in various classrooms
- and others 

This description outlines the institution we will be working with. We will not be handling all processes related to the operation of the university. 
We are to focus on managing processes related to the recruitment of candidates for studies and the promotion of students to the next semester.

## Business requirements
![plot](./docs/images/process.png)
