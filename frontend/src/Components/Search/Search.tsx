import React, { ChangeEvent, useState, SyntheticEvent } from 'react'

type Props = {}

const Search : React.FC<Props> = (props: Props) : JSX.Element => {
    // to use typescript<string> generics
  const [search, setSearch] = useState<string>("");
  
  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
    console.log(e);
    // console.log(search);
  }

  // mouse event can cause problem, syntheticEvent is generic with type checking
  const onClick = (e: SyntheticEvent) => {
    console.log(e);
  }

  return (
    <div>
        <input value={search} onChange={(e) => handleChange(e)}></input>
        <button onClick={(e) => onClick(e)} />
    </div>
  )
}

export default Search;