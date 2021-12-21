export class BlogComment{

    constructor(
        public blogCommentId: number,
        public blogId: string,
        public content: string,
        public username: string,
        public applicationUserId: number,
        public publishDate: Date,
        public updateDate: Date,
        public parentBlogCommentId?: number,
    ){}
}