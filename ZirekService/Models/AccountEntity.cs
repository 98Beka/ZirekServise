﻿using Microsoft.AspNetCore.Identity;

namespace ZirekService.Models {
    public class AccountEntity {
        public int Id { get; set; }
        public int level { get; set; }
        public List<WordsNodeEntity> wordsNodes { get; set; }
        public string IdentityUserId { get; set; }
    }
}
