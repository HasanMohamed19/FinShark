import { useState, ChangeEvent, SyntheticEvent } from 'react';
import './App.css';
import CardList from './Components/CardList/CardList';
import Search from './Components/Search/Search';
import { CompanySearch } from './company';
import { searchCompanies } from './api';
import ListPortfolio from './Components/Portfolio/ListPortfolio/ListPortfolio';

function App() {

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
      const exists = portfolioValues.find((value) => value === e.target.value);
      if (exists) return;

      const updatedPortfolio = [...portfolioValues, e.target[0].value];
      setPortfolioValues(updatedPortfolio);
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
      <ListPortfolio portfolioValues={portfolioValues}/>
      {serverError && <h1>{serverError}</h1>}
      <CardList searchResults={searchResult} onPortfolioCreate={onPortfolioCreate}/>
    </div>
  );
}

export default App;
