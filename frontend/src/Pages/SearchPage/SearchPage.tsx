import React, { ChangeEvent, SyntheticEvent, useState } from 'react'
import CardList from '../../Components/CardList/CardList';
import Navbar from '../../Components/Navbar/Navbar';
import ListPortfolio from '../../Components/Portfolio/ListPortfolio/ListPortfolio';
import { searchCompanies } from '../../api';
import { CompanySearch } from '../../company';
import Search from '../../Components/Search/Search';

interface Props {}

const SearchPage = (props: Props) => {

    
    // to use typescript<string> generics
    const [search, setSearch] = useState<string>("");
    const [portfolioValues, setPortfolioValues] = useState<string[]>([]);
    const [searchResult, setSearchResult] = useState<CompanySearch[]>([]);
    const [serverError, setServerError] = useState<string>("");
  
    const handleSearchChange = (e: ChangeEvent<HTMLInputElement>) => {
      setSearch(e.target.value);
      console.log(e);
      // console.log(search);
    }

    const onPortfolioCreate = (e: any) => {
      e.preventDefault();

      // ensure no duplicates
      const exists = portfolioValues.find((value) => value === e.target[0].value);
      if (exists) return;
      
      // since react requires immutablity, to update array/object create a new one
      const updatedPortfolio = [...portfolioValues, e.target[0].value];
      setPortfolioValues(updatedPortfolio);
    }

    const onPortfolioDelete = (e: any) => {
      e.preventDefault();
      const removed = portfolioValues.filter((value) => {
        return value !== e.target[0].value;
      })
      setPortfolioValues(removed);
    }
  
    // mouse event can cause problem, syntheticEvent is generic with type checking
    const onSearchSubmit = async (e: SyntheticEvent) => {
      e.preventDefault();
      const result = await searchCompanies(search);
      
      if (typeof result === "string") {
        setServerError(result);
      } else if (Array.isArray(result.data)) {
        setSearchResult(result.data);
      }
      console.log(searchResult);
    }

  return (
    <div className="App">
      <Search onSearchSubmit={onSearchSubmit} search={search} handleSearchChange={handleSearchChange}/>
      <ListPortfolio portfolioValues={portfolioValues} onPortfolioDelete={onPortfolioDelete}/>
      {serverError && <h1>{serverError}</h1>}
      <CardList searchResults={searchResult} onPortfolioCreate={onPortfolioCreate}/>
    </div>
  )
}

export default SearchPage;