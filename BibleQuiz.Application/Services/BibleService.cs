﻿using BibleQuiz.Domain.Models;
using BibleQuiz.Domain.Services;
using BibleQuiz.Domain.Shared;
using Microsoft.Extensions.Options;

namespace BibleQuiz.Application.Services;
public class BibleService : IBibleService
{
    private readonly IHttpClient _httpClient;

    private readonly BibleCredentials _bibleCreds;

    public BibleService(IHttpClient httpClient, IOptions<BibleCredentials> bibleCreds)
    {
        _httpClient = httpClient;
        _bibleCreds = bibleCreds.Value;
    }

    public async Task<Result<BibleVerseSearchResponse, Error>> GetScripturesAsync(BibleVerse model)
    {
        var headers = new Dictionary<string, string>()
        {
            {"api-key", _bibleCreds.ApiKey }
        };

        try
        {
            var bibleVerse = await _httpClient.SendAsync<BibleVerse, BibleVerseSearchResponse, Error>(
               new HttpRequest<BibleVerse>
               {
                   Uri = _bibleCreds.Url + _bibleCreds.KjvSpec + $"/search?query={model.Verse}",
                   Method = HttpMethod.Get,
                   Headers = headers
               });

            return bibleVerse;
        }

        catch(Exception ex)
        {
            return new BibleVerseSearchResponse
            {
                Data = new ScriptureData
                {
                    Passages = new List<Passages>
                    {
                        new Passages
                        {
                            Content = "Unable to get this scripture at the moment"
                        }
                    }
                }
            };
        }

       
    }

    public async Task<Result<BibleBooks, Error>> GetBibleBooks()
    {
        var headers = new Dictionary<string, string>()
        {
            {"api-key", _bibleCreds.ApiKey }
        };

        var books = await _httpClient.SendAsync<object, BibleBooks, Error>(new HttpRequest<object>
        {
            Uri = _bibleCreds.Url + _bibleCreds.KjvSpec + "/books",
            Method = HttpMethod.Get,
            Headers = headers
        });

        return books;
    }
}
