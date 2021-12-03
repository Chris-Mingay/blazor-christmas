<script>

	import { onMount} from 'svelte';

	let name

	onMount(async() => {
		const res = await fetch('https://localhost:5021/api/UserProfiles',{
			headers: {
				'Authorization': `Bearer ${localStorage.getItem('jwt')}`
			}
		});
		console.log(res);
		const json = await res.json();
		name = json.data.name;
	})
</script>

{#if name}
	<h2>{name}</h2>
{/if}